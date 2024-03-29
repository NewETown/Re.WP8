﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Re.Model
{
    public class WebResult : INotifyPropertyChanged
    {
        private string _id;
        public string Id 
        { 
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _displayUrl;
        public string DisplayUrl
        {
            get { return _displayUrl; }
            set
            {
                if (_displayUrl != value)
                {
                    _displayUrl = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                if (_url != value)
                {
                    _url = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _keywords;
        public string Keywords
        {
            get { return _keywords; }
            set
            {
                if (_keywords != value)
                {
                    _keywords = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string TileColor
        {
            get
            {
                string[] colorArray = { "6DC2E9", "00D8CC", "55D455" }; // "CAEAEA", "87CEFA", "66CDAA", "90EE90" };
                Random random = new Random();
                int num = random.Next(0, (colorArray.Length - 1));
                return "#" + colorArray[num];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
