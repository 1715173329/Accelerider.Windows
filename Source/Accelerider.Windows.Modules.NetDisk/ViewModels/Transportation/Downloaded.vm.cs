﻿using System.Linq;
using Accelerider.Windows.Infrastructure;
using Accelerider.Windows.Modules.NetDisk.Models;
using Accelerider.Windows.TransferService;
using Unity;


namespace Accelerider.Windows.Modules.NetDisk.ViewModels.Transportation
{
    public class DownloadedViewModel : TransferredBaseViewModel
    {
        private readonly IDownloaderManager _downloaderManager;

        public DownloadedViewModel(IUnityContainer container, IDownloaderManager downloaderManager) : base(container)
        {
            _downloaderManager = downloaderManager;

            EventAggregator.GetEvent<TransferItemCompletedEvent>().Subscribe(
                item => TransferredFiles.Add(item),
                Prism.Events.ThreadOption.UIThread,
                true,
                _ => TransferredFiles != null);
        }

        public override void OnLoaded()
        {
            if (TransferredFiles == null)
            {
                TransferredFiles = new ObservableSortedCollection<ILocalDiskFile>(
                    AcceleriderUser
                        .GetNetDiskUsers()
                        .SelectMany(item => item.GetDownloadedFiles()),
                    DefaultTransferredFileComparer);
            }
        }
    }
}
