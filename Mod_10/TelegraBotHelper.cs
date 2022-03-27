using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Telegram.Bot;

namespace Mod_10
{
    public class TelegraBotHelper
    {
        private MainWindow _window;
        private MessageReader _messageReader;
        private Button _button;
        private TelegramBotClient _client;

        private Telegram.Bot.Types.Update _e;
        IEnumerable<IGrouping<string, FileInfo>> _queryGroupByExt;

        private string _fileExtension;
        private string _filePath;
        private string _fileMessage;
        private bool _callback;
        private string _path;
        private readonly string _token;

        /// <summary>
        /// токен
        /// </summary>
        /// <param name="token"></param>
        public TelegraBotHelper(MainWindow window, ListBox logList)
        {
            _window = window;
            _token = File.ReadAllText(Environment.CurrentDirectory + @"\Token_bot.txt");
            _button = new Button();
            _messageReader = new MessageReader(_window, logList);
        }

        public ObservableCollection<Message> GetMessageCollection(long id)
        {
            return _messageReader.GetMessageCollection(id);
        }
        public async void PushBotMessageAdmin(string text, long id)
        {
            await _client.SendTextMessageAsync(id, text);
        }
        public void PushCollectionAdmin(string text, long id)
        {
            _messageReader.MessageLog(id, "Admin", text);
        }

        /// <summary>
        /// проверка на наличие новых данных + Timeout
        /// </summary>
        internal async void GetUpdates()
        {

            await Task.Run(() =>
            {
                if (_path == null)
                {
                    _path = Environment.CurrentDirectory;
                    Directory.CreateDirectory(_path + "\\File\\");
                }

                _client = new Telegram.Bot.TelegramBotClient(_token);
                var me = _client.GetMeAsync().Result;
                if (me != null && !string.IsNullOrEmpty(me.Username))
                {
                    int offset = 0;
                    while (true)
                    {
                        try
                        {
                            var updates = _client.GetUpdatesAsync(offset).Result;
                            if (updates != null && updates.Count() > 0)
                            {
                                foreach (var e in updates)
                                {
                                    this._e = e;
                                    MessageReader();
                                    offset = e.Id + 1;
                                    if (e.Message != null)
                                    {
                                        _messageReader.MessageLog(e.Message.Chat.Id, e.Message.Chat.FirstName, e.Message.Text);
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        Thread.Sleep(1000);
                    }
                }
            });

        }

        /// <summary>
        /// Get message collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        /// <summary>
        /// определения типа входных данных
        /// </summary>
        private async void MessageReader()
        {
            var msg = _e.Message;

            switch (_e.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    if (msg.Text != null)
                    {
                        WorkingWithArchive(msg.Text);
                    }
                    break;
                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    if (this._callback)
                    {
                        await _client.SendTextMessageAsync(_e.CallbackQuery.Message.Chat.Id, $"выбранно: {_e.CallbackQuery.Data}");
                        DocProcessing();
                    }
                    else
                    {
                        Callback(_e.CallbackQuery.Data);
                    }

                    break;
                default:
                    Console.WriteLine("необработанный тип данных");
                    break;
            }
            if (_e.Message != null)
            {
                TypeFile();
            }
            ConsoleStatus();
        }

        /// <summary>
        /// вывод данных в консоль
        /// </summary>
        private void ConsoleStatus()
        {
            if (_e.Message != null)
            {
                string text = $"{DateTime.Now.ToLongTimeString()}:  {_e.Message.Chat.FirstName} {_e.Message.Chat.Id} {_e.Message.Text}";
                Console.WriteLine($"{text} TypeMessage: {_e.Message.Type.ToString()}");
            }
        }

        /// <summary>
        /// Работа с архивом файлов
        /// </summary>
        /// <param name="text"></param>
        /// <param name="id"></param>
        private async void WorkingWithArchive(string text)
        {
            _callback = false;
            Download dow = new Download(_e, _client, _fileExtension, _fileMessage, _filePath);
            long id = _e.Message.Chat.Id;
            switch (text)
            {
                case "/start":
                    await _client.SendTextMessageAsync(id, "Привет", replyMarkup: _button.GetButtonse());
                    break;
                case "Архив Фото":
                    WorkingWithFile("*.jpeg");
                    break;
                case "Аудио сообщение":
                    WorkingWithFile("*.ogg");
                    break;
                case "Документы":
                    WorkingWithDocuments();
                    break;
                case "Видео":
                    WorkingWithFile("*.mp4");
                    break;
                default:
                    if (this._filePath == null)
                    {
                        await _client.SendTextMessageAsync(id, "Привет, я не понимаю тебя, возможно я еще не умею делать то чего ты хочешь, обратись к моему создателю и возможно он научит меня тому что тебе нужно:) для Запуска нажми: start",
                        replyMarkup: _button.GetButtonseStart());
                    }
                    else
                    {
                        dow.UploadingFile();
                        this._filePath = null;
                    }
                    break;
            }
        }
        /// <summary>
        /// сохронение файлов из ТБ
        /// </summary>
        private async void TypeFile()
        {
            switch (_e.Message.Type)
            {
                case Telegram.Bot.Types.Enums.MessageType.Photo:
                    string fileIdPhoto = _e.Message.Photo[_e.Message.Photo.Length - 1].FileId;
                    await _client.SendTextMessageAsync(_e.Message.Chat.Id, "Введите название фотографии или нажмите кнопку дата и фото будет присвоено названия текущего времени и даты по МСК", replyMarkup: _button.GetButtonseDate());
                    this._fileExtension = ".jpeg";
                    this._filePath = fileIdPhoto;
                    this._fileMessage = "Фото загружено";
                    break;
                case Telegram.Bot.Types.Enums.MessageType.Video:
                    string formatVideo = _e.Message.Video.MimeType;
                    formatVideo = Expansion(formatVideo);
                    this._fileExtension = $".{formatVideo}";
                    this._filePath = _e.Message.Video.FileId;
                    this._fileMessage = "Видео загружено";
                    await _client.SendTextMessageAsync(_e.Message.Chat.Id, "Введите название видео или нажмите кнопку дата и фото будет присвоено названия текущего времени и даты по МСК", replyMarkup: _button.GetButtonseDate());
                    break;
                case Telegram.Bot.Types.Enums.MessageType.Voice:
                    string formatVoice = _e.Message.Voice.MimeType;
                    formatVoice = Expansion(formatVoice);
                    this._fileExtension = $".{formatVoice}";
                    this._filePath = _e.Message.Voice.FileId;
                    this._fileMessage = "Аудио запись загружена";
                    await _client.SendTextMessageAsync(_e.Message.Chat.Id, "Введите название аудио записи или нажмите кнопку дата и фото будет присвоено названия текущего времени и даты по МСК", replyMarkup: _button.GetButtonseDate());
                    break;
                case Telegram.Bot.Types.Enums.MessageType.Document:
                    string formatDoc = _e.Message.Document.MimeType;
                    formatDoc = Expansion(formatDoc);
                    this._fileExtension = $".{formatDoc}";
                    this._filePath = _e.Message.Document.FileId;
                    this._fileMessage = "Документ загружен";
                    await _client.SendTextMessageAsync(_e.Message.Chat.Id, "Введите название Файла или нажмите кнопку дата и фото будет присвоено названия текущего времени и даты по МСК", replyMarkup: _button.GetButtonseDate());
                    break;
            }
        }
        /// <summary>
        /// возврат формата файла
        /// </summary>
        /// <param name="expansion"></param>
        /// <returns></returns>
        private string Expansion(string expansion)
        {
            return expansion = expansion.Substring(expansion.IndexOf('/') + 1);
        }
        /// <summary>
        /// поиск, отправка файлов
        /// </summary>
        /// <param name="a">chat ID</param>
        private async void WorkingWithFile(string type)
        {

            long id = _e.Message.Chat.Id;
            string[] fotoList = Directory.GetFiles(this._path + "\\File", type);
            Dictionary<int, string> path = new Dictionary<int, string>();
            int inckrement = 0;
            if (fotoList.Length == 0)
            {
                await _client.SendTextMessageAsync(id, "Упс, похоже архив пуст");
            }
            else
            {
                foreach (var item in fotoList)
                {
                    inckrement++;
                    path.Add(inckrement, item);

                    FileInfo file = new FileInfo(item);
                    string data = file.Extension.ToLower();
                    data = string.Join(",", file.Name, data);
                    var r = _client.SendTextMessageAsync(id, file.Name, replyMarkup: _button.GetInLineButton(data)).Result;
                }
            }
        }
        /// <summary>
        /// опеределения формата файла дял загрузки
        /// </summary>
        /// <param name="path"></param>
        private void Callback(string path)
        {
            Download dow = new Download(_e, _client, _fileExtension, _fileMessage, _filePath);
            string[] data = path.Split(',');
            if (data.Length == 2)
            {
                switch (data[1])
                {
                    case ".jpeg":
                        dow.DownloadPhoto(this._path + "\\File\\" + data[0]);
                        break;
                    case ".mp4":
                        dow.DownloadVideo(this._path + "\\File\\" + data[0]);
                        break;
                    case ".ogg":
                        dow.DownloadAudio(this._path + "\\File\\" + data[0]);
                        break;
                    default:
                        dow.DownLoadFile(this._path + "\\File\\" + data[0]);
                        break;
                }
            }
        }
        /// <summary>
        /// сортировка по расширениям
        /// </summary>
        private void WorkingWithDocuments()
        {

            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory + "\\File");
            IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);
            var queryGroupByExt =
                from file in fileList
                group file by file.Extension.ToLower() into fileGroup
                orderby fileGroup.Key
                select fileGroup;

            this._queryGroupByExt = queryGroupByExt;
            KeyProcessing();
        }
        /// <summary>
        /// вывод скиска формата файлов
        /// </summary>
        /// <param name="trimLength"></param>
        private async void KeyProcessing()
        {
            int count = _queryGroupByExt.Count();
            if (count != 0)
            {
                long id = _e.Message.Chat.Id;
                foreach (var fileGroup in this._queryGroupByExt)
                {
                    var r = _client.SendTextMessageAsync(id, $"расширение: {fileGroup.Key}", replyMarkup: _button.GetInLineButtonFile(fileGroup.Key)).Result;
                }
            }
            else
            {
                await _client.SendTextMessageAsync(_e.Message.Chat.Id, "Упс, похоже архив пуст");
            }
            this._callback = true;
        }
        /// <summary>
        /// вывод файлов по ключу
        /// </summary>
        private void DocProcessing()
        {

            foreach (var fileGroup in this._queryGroupByExt)
            {
                if (_e.CallbackQuery != null && fileGroup.Key == _e.CallbackQuery.Data)
                {
                    foreach (var item in fileGroup)
                    {
                        FileInfo type = new FileInfo(Convert.ToString(item));
                        string dataDoc = type.Extension.ToLower();
                        dataDoc = string.Join(",", Convert.ToString(item.Name), dataDoc);
                        var dok = _client.SendTextMessageAsync(_e.CallbackQuery.Message.Chat.Id, Convert.ToString(item), replyMarkup: _button.GetInLineButton(dataDoc)).Result;
                    }
                }
            }
            this._callback = false;
        }
    }
}
