﻿using System.Collections.Generic;
using Accelerider.Windows.Modules.NetDisk.Models.BaiduCloud;
using Accelerider.Windows.Modules.NetDisk.Models.OneDrive;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Accelerider.Windows.Modules.NetDisk.Models.Results
{
    public class BaiduListFileResult : ResultBase
    {
        [JsonProperty("list")]
        public List<BaiduCloudFile> FileList { get; set; }
    }

    public class OneDriveListFileResult : ResultBase
    {
        [JsonProperty("value")]
        public List<OneDriveFile> FileList { get; set; }

        [JsonProperty("@odata.nextLink")]
        public string NextPage { get; set; }
    }

    public class MicrosoftUserInfoResult : ResultBase
    {
        [JsonProperty("owner")]
        public JToken Owner { get; set; }

        [JsonProperty("quota")]
        public JToken Quota { get; set; }
    }

    public class SixCloudLoginResult : ResultBase
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }


}
