using System.Windows.Forms;

namespace Rowatch2.Librarys
{
    internal class LabelRefreshTimer
    {
        private Label _control;
        private Timer _timer;

        public LabelRefreshTimer(Label control, int interval)
        {
            _control = control;

            _timer = new Timer();
            _timer.Interval = interval;
            _timer.Tick += new System.EventHandler(timer_Tick);
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            SetText(_text);
        }
        
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
            }
        }

        public void SetText(string text)
        {
            if (_control.Text != text)
            {
                _control.Text = text;
            }
        }
    }
}