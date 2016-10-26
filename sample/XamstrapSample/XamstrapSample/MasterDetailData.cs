using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XamstrapSample
{
    public class MasterDetailData : INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        private string _selectedEmail;

        public string SelectedEmail
        {
            get { return _selectedEmail; }
            set
            {
                
                SetProperty(ref _selectedEmail, value);
                IsMasterVisible = false;                
            }
        }

        private bool _isMasterVisible;

        public bool IsMasterVisible
        {
            get { return _isMasterVisible; }
            set
            {
                SetProperty(ref _isMasterVisible, value);
            }
        }

        #endregion
        public MasterDetailData()
        {
            SelectedEmail ="No email selected yet!!";
            Emails = new List<string>() {
                "a@b.com","b@c.com","c@d.com","d@e.com","e@f.com",
                "f@g.com","g@h.com","h@i.com","i@j.com","j@k.com",
                "k@l.com","l@m.com","m@n.com","n@o.com","o@p.com"
            };
        }
        public List<String> Emails { get; set; }

        #region Private Methods
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
