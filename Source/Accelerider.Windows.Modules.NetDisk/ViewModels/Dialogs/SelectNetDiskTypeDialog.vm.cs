﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Accelerider.Windows.Infrastructure;
using Accelerider.Windows.Infrastructure.Mvvm;
using Accelerider.Windows.Modules.NetDisk.ViewModels.Others;
using Accelerider.Windows.Modules.NetDisk.Views.Dialogs;
using Accelerider.Windows.Modules.NetDisk.Views.NetDiskAuthentications;
using MaterialDesignThemes.Wpf;
using Unity;


namespace Accelerider.Windows.Modules.NetDisk.ViewModels.Dialogs
{
    public class SelectNetDiskTypeDialogViewModel : ViewModelBase, IViewLoadedAndUnloadedAware<SelectNetDiskTypeDialog>
    {
        private IEnumerable<NetDiskType> _netDiskTypes;
        private ICommand _selectNetDiskCommand;


        public SelectNetDiskTypeDialogViewModel(IUnityContainer container) : base(container)
        {
            NetDiskTypes = InitializeNetDiskTypes();
            SelectNetDiskCommand = new RelayCommand<NetDiskType>(SelectNetDiskCommandExecute);
        }

        public IEnumerable<NetDiskType> NetDiskTypes
        {
            get => _netDiskTypes;
            set => SetProperty(ref _netDiskTypes, value);
        }

        public ICommand SelectNetDiskCommand
        {
            get => _selectNetDiskCommand;
            set => SetProperty(ref _selectNetDiskCommand, value);
        }


        private void SelectNetDiskCommandExecute(NetDiskType netDiskType)
        {
        }

        // HACK: Mock data.
        private IEnumerable<NetDiskType> InitializeNetDiskTypes()
        {
            return new[]
            {
                new NetDiskType
                {
                    Logo = new BitmapImage(new Uri(@"..\..\Images\BaiduCloudLogo.png", UriKind.Relative)),
                    Name = "Baidu Cloud",
                    Description = "XXXXXXXXXXXXXXXXXXXXXXXXXXX"
                },
                new NetDiskType
                {
                    Logo = new BitmapImage(new Uri(@"..\..\Images\OneDriveLogo.jpg", UriKind.Relative)),
                    Name = "OneDrive",
                    Description = "XXXXXXXXXXXXXXXXXXXXXXXXXXX"
                },
                new NetDiskType
                {
                    Logo = new BitmapImage(new Uri(@"..\..\Images\OneDriveLogo.jpg", UriKind.Relative)),
                    Name = "Six Cloud",
                    Description = "XXXXXXXXXXXXXXXXXXXXXXXXXXX",
                    OpenCommand = new RelayCommand(async() =>
                    {
                        await TimeSpan.FromMilliseconds(1000);
                        await DialogHost.Show(new SixCloud(), "RootDialog");
                    })
                },
            };
        }

        void IViewLoadedAndUnloadedAware<SelectNetDiskTypeDialog>.OnLoaded(SelectNetDiskTypeDialog view)
        {
            DialogHost.SetDialogClosingAttached(view, (sender, args) => {});
        }

        void IViewLoadedAndUnloadedAware<SelectNetDiskTypeDialog>.OnUnloaded(SelectNetDiskTypeDialog view)
        {
        }
    }
}
