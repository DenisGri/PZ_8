using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Documents;

namespace PZ_8
{
    [Serializable]
    public struct Student : INotifyPropertyChanged
    {
        private string _fio;
        private string _group;
        private int[] _scores = new int[5];

        public string Fio
        {
            get => _fio;
            set
            {
                _fio = value;
                NotifyPropertyChanged();
            }
        }

        public string Group
        {
            get => _group;
            set
            {
                _group = value;
                NotifyPropertyChanged();
            }
        }

        public int[] Scores
        {
            get => _scores;
            set
            {
                _scores = value;
                NotifyPropertyChanged();
            }
        }

        public string GetStringScores => string.Join(',', _scores);

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}