﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Accelerider.Windows.Modules.NetDisk.Models.Results;
using Refit;
// ReSharper disable StringLiteralTypo

namespace Accelerider.Windows.Modules.NetDisk.Models.BaiduCloud
{
    [Headers("User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36",
             "Referer: https://pan.baidu.com/disk/home")]
    public interface IBaiduCloudApi
    {
        [Get("/disk/home")]
        Task<string> GetHomePageAsync();

        [Get("/api/list?channel=chunlei&clienttype=0&web=1&num=100&order=name")]
        Task<BaiduListFileResult> GetFileListAsync(string bdsToken, [AliasAs("dir")] string path, int page);

        [Post("/api/filemanager?opera=delete&channel=chunlei&web=1&app_id=250528&clienttype=8")]
        Task<ResultBase> DeleteFileAsync(string bdsToken, string logId, [Body(BodySerializationMethod.UrlEncoded)]
            Dictionary<string, string> fileList);

    }
}
