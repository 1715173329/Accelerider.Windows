﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Accelerider.Windows.Infrastructure;
using Accelerider.Windows.Infrastructure.Mvvm;
using Accelerider.Windows.Modules.NetDisk.Models;
using Unity;


namespace Accelerider.Windows.Modules.NetDisk.ViewModels.FileBrowser
{
    public abstract class LoadingFilesBaseViewModel<T> : ViewModelBase, IViewLoadedAndUnloadedAware
    {
        private bool _isLoadingFiles;
        private ICommand _refreshFilesCommand;
        private IList<T> _files;

        protected LoadingFilesBaseViewModel(IUnityContainer container) : base(container)
        {
            RefreshFilesCommand = new RelayCommand(async () => await LoadingFilesAsync());
        }

        public ICommand RefreshFilesCommand
        {
            get => _refreshFilesCommand;
            set => SetProperty(ref _refreshFilesCommand, value);
        }

        public bool IsLoadingFiles
        {
            get => _isLoadingFiles;
            set { if (SetProperty(ref _isLoadingFiles, value)) EventAggregator.GetEvent<IsLoadingFilesChangedEvent>().Publish(_isLoadingFiles); }
        }

        public IList<T> Files
        {
            get => _files;
            private set => SetProperty(ref _files, value);
        }

        protected INetDiskUser PreviousNetDiskUser { get; set; }

        public virtual void OnLoaded()
        {
            if (PreviousNetDiskUser != CurrentNetDiskUser)
            {
                OnCurrentNetDiskUserChanged(CurrentNetDiskUser);
            }
        }

        public virtual void OnUnloaded()
        {
            PreviousNetDiskUser = CurrentNetDiskUser;
        }

        protected async Task LoadingFilesAsync(IList<T> files = null)
        {
            if (IsLoadingFiles) return;

            IsLoadingFiles = true;
            if (files == null)
            {
                var task = GetFilesAsync();
                if (task != null) Files = await task;
            }
            else
            {
                Files = files;
            }
            IsLoadingFiles = false;
        }

        protected abstract Task<IList<T>> GetFilesAsync();

        protected override async void OnCurrentNetDiskUserChanged(INetDiskUser currentNetDiskUser)
        {
            await currentNetDiskUser.RefreshAsync();
            await LoadingFilesAsync();
        }
    }
}
