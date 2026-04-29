using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;
using static YooAsset.DownloaderOperation;

namespace Framework.Events
{
    /// <summary>
    /// 开始下载文件事件
    /// </summary>
    public struct OnDownloadFileBeginEvent
    {
        public DownloadFileData downloadFileData;
    }

    /// <summary>
    /// 下载更新事件
    /// </summary>
    public struct OnDownloadUpdateEvent
    {
        /// <summary>
        /// 当下载进度数据
        /// </summary>
        public DownloadUpdateData downloadUpdateData;
    }

    /// <summary>
    /// 下载完成事件
    /// </summary>
    public struct OnDownloadFinishEvent
    {
        //下载完成数据
        public DownloaderFinishData downloaderFinishData;
    }

    /// <summary>
    /// 下载错误事件
    /// </summary>
    public struct OnDownloadErrorEvent
    {
        //下载错误数据
        public DownloadErrorData errorData;
    }

    /// <summary>
    /// 场景加载进度
    /// </summary>
    public struct OnSceneloadUpdateEvent
    {
        /// <summary>
        /// 加载进度
        /// </summary>
        public float progress;
        /// <summary>
        /// 描述信息
        /// </summary>
        public string desc;
    }

    /// <summary>
    /// 文件现在信息（大小/数量）
    /// </summary>
    public struct OnDownloadInfoHandlerEvent
    {
        /// <summary>
        /// 下载的文件数量
        /// </summary>
        public int totalDownloadCount;
        /// <summary>
        /// 文件总大小
        /// </summary>
        public long totalDownloadBytes;
        /// <summary>
        /// 信息确认回调
        /// </summary>
        public Action confirmCallBack;
        /// <summary>
        /// 信息取消回调
        /// </summary>
        public Action cancelCallBack;
    }

    /// <summary>
    /// 下载取消请求事件
    /// </summary>
    public struct OnDownloadCancelRequestEvent
    {
        /// <summary>
        /// 取消原因
        /// </summary>
        public string reason;
    }

    /// <summary>
    /// 下载取消事件
    /// </summary>
    public struct OnDownloadCanceledEvent
    {
        /// <summary>
        /// 取消原因
        /// </summary>
        public string reason;
    }

    /// <summary>
    /// 资源加载进度
    /// </summary>
    public struct OnAssetloadProgressEvent
    {
        /// <summary>
        /// 当前加载进度
        /// </summary>
        public float progress;
        /// <summary>
        /// 描述信息
        /// </summary>
        public string desc;
    }
}
