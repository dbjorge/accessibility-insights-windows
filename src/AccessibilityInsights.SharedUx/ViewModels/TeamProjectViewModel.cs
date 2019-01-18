// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using AccessibilityInsights.Extensions.Interfaces.BugReporting;
using System.Collections.Generic;
using System.Windows;

namespace AccessibilityInsights.SharedUx.ViewModels
{
    /// <summary>
    /// ViewModel class for entity in server hierarchy
    /// </summary>
    public class TeamProjectViewModel : ViewModelBase
    {
        /// <summary>
        /// The server-based Project associated with this view
        /// </summary>
        public IProject Project { get; set; }

        /// <summary>
        /// The server-based Team associated with this view
        /// </summary>
        public ITeam Team { get; set; }

        public string Name => Project == null ? Team.Name : Project.Name;

        private Visibility _visibility;
        public Visibility Visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        private bool _expanded;
        public bool Expanded
        {
            get
            {
                return _expanded;
            }
            set
            {
                _expanded = value;
                OnPropertyChanged(nameof(Expanded));
            }
        }

        private List<TeamProjectViewModel> _children;
        public List<TeamProjectViewModel> Children
        {
            get
            {
                return _children;
            }
            internal set
            {
                _children = value;
                OnPropertyChanged(nameof(Children));
            }
        }

        public TeamProjectViewModel(IProject project, List<TeamProjectViewModel> children)
        {
            this.Project = project;
            this._children = children ?? new List<TeamProjectViewModel>();
        }

        public TeamProjectViewModel(ITeam team, List<TeamProjectViewModel> children)
        {
            this.Team = team;
            this._children = children ?? new List<TeamProjectViewModel>();
        }
    }
}