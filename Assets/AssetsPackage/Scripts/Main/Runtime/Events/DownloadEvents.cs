using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;
using static YooAsset.DownloaderOperation;

namespace MsbFramework.Events
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
    }
}
