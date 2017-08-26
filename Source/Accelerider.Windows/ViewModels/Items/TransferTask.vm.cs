﻿using System;
using System.Threading.Tasks;
using Accelerider.Windows.Events;
using Accelerider.Windows.Infrastructure;
using Accelerider.Windows.Infrastructure.Interfaces;

namespace Accelerider.Windows.ViewModels.Items
{
    public class TransferTaskViewModel : BindableBase
    {
        private string _ownerName;
        private DataSize _progress;
        private TimeSpan? _remainingTime;
        private DataSize _speed;

        private bool _isBusy;


        public TransferTaskViewModel(TaskCreatedEventArgs taskInfo)
        {
            OwnerName = taskInfo.OwnerName;
            Token = taskInfo.Token;
            Token.TransferStateChanged += (sender, e) => OnPropertyChanged(nameof(TransferState));

            RefreshTransferState();
        }


        public string OwnerName
        {
            get => _ownerName;
            set => SetProperty(ref _ownerName, value);
        }

        public ITransferTaskToken Token { get; }

        public IDiskFile FileInfo => Token.FileInfo;

        public TransferStateEnum TransferState => Token.TransferState;

        public DataSize Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public DataSize Speed
        {
            get => _speed;
            set => SetProperty(ref _speed, value);
        }

        public TimeSpan? RemainingTime
        {
            get => _remainingTime;
            set => SetProperty(ref _remainingTime, value);
        }


        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }


        private async void RefreshTransferState()
        {
            while (true)
            {
                await Task.Delay(1000);
                UpdateTransferState();
            }
        }

        private void UpdateTransferState()
        {
            Speed = Token.Progress - Progress;
            Progress = Token.Progress;
            if (Speed.BaseBValue == 0)
                RemainingTime = null;
            else
                RemainingTime = TimeSpan.FromSeconds(Math.Round((Token.FileInfo.FileSize - Progress) / Speed));
        }
    }
}
