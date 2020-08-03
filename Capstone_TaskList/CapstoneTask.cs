using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone_TaskList
{
    class CapstoneTask
    {
        #region Fields
        private string _teamMemberName;
        private string _taskDesc;
        private DateTime _dueDate;
        private bool _taskStatus;
        #endregion

        #region Properties
        public string TeamMemberName
        {
            get { return _teamMemberName; }
            set { _teamMemberName = value; }
        }

        public string TaskDesc
        {
            get { return _taskDesc; }
            set { _taskDesc = value; }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }
        public bool TaskStatus
        {
            get { return _taskStatus; }
            set { _taskStatus = value; }
        }

        #endregion

        #region Constructors
        public CapstoneTask() { }

        public CapstoneTask(string TeamMemberName, string TaskDesc, DateTime DueDate)
        {
            _teamMemberName = TeamMemberName;
            _taskDesc = TaskDesc;
            _dueDate = DueDate;
        }

        public CapstoneTask(string TeamMemberName, string TaskDesc, DateTime DueDate, bool TaskStatus)
        {
            _teamMemberName = TeamMemberName;
            _taskDesc = TaskDesc;
            _dueDate = DueDate;
            _taskStatus = TaskStatus;
        }
        #endregion

        #region Methods
        public void Complete()
        {
            TaskStatus = true;
        }
        #endregion
    }
}
