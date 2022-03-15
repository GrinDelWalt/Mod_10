
namespace Mod_10
{
    public class Message
    {
        public Message(string text, string date, string name)
        {
            this.text = text;
            this.date = date;
            this.name = name;
        }

        public string Text { get { return this.text; } set { this.text = value; } }

        public string Date { get { return this.date; } set { this.date = value; } }

        public string Name { get { return this.name; } set { this.name = value; } }

        private string name;
        private string text;
        private string date;
    }
}
