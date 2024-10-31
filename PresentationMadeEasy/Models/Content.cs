using Microsoft.UI.Text;
using System;
using System.ComponentModel;

namespace PresentationMadeEasy.Models
{
    public class Content : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return (string.IsNullOrEmpty(title)) ? string.Empty : $"{title[0].ToString().ToUpper()}{title.Substring(1)}";
            }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public string Paragraph { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static implicit operator string(Content v)
        {
            throw new NotImplementedException();
        }
    }
}
