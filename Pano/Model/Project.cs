using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace Pano.Model
{
    public class Project : ObservableObject
    {
        private Guid _guid = Guid.NewGuid();
        private string _name = "";
        private string _description = "";
        private DateTime _dateOfCreation = DateTime.MinValue;
        private DateTime _dateOfLastModification = DateTime.MinValue;

        public Project()
        {
            var now = DateTime.Now;
            DateOfCreation = now;
            DateOfLastModification = now;
        }

        public Guid Guid
        {
            get => _guid;
            set
            {
                Set(ref _guid, value);
                RaisePropertyChanged(nameof(GuidString));
            }
        }

        public string GuidString => _guid.ToString();

        public string Name
        {
            get => _name;
            set
            {
                Set(ref _name, value);
            }
        }

        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        public DateTime DateOfCreation
        {
            get => _dateOfCreation;
            set => Set(ref _dateOfCreation, value);
        }

        public DateTime DateOfLastModification
        {
            get => _dateOfLastModification;
            set => Set(ref _dateOfLastModification, value);
        }
    }
}
