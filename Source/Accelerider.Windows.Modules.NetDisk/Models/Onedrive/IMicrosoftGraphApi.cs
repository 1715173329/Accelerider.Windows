﻿using System.Threading.Tasks;
using Accelerider.Windows.Modules.NetDisk.Models.Results;
using Refit;

namespace Accelerider.Windows.Modules.NetDisk.Models.OneDrive
{
    [Headers("User-Agent: Accelerider.Windows.Wpf: v1.0.0-pre",
		     "Authorization: Bearer")]
	public interface IMicrosoftGraphApi
	{
		[Get("/v1.0/me/drive")]
		Task<MicrosoftUserInfoResult> GetUserInfoAsync();

		[Get("/v1.0/me/drive/root/children")]
		Task<OneDriveListFileResult> GetRootFilesAsync();

		[Get("/v1.0/me/drive/root:{path}:/children")]
		Task<OneDriveListFileResult> GetFilesAsync(string path);

		[Delete("/v1.0/me/drive/root:{path}")]
		Task<ResultBase> DeleteFileAsync(string path);
	}
}
