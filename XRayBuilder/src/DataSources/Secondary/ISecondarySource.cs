﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using JetBrains.Annotations;
using XRayBuilderGUI.DataSources.Secondary.Model;
using XRayBuilderGUI.Libraries.Progress;
using XRayBuilderGUI.Model;
using XRayBuilderGUI.XRay.Artifacts;

namespace XRayBuilderGUI.DataSources.Secondary
{
    // TODO Consider splitting sources into terms/xray vs metadata/extras
    public interface ISecondarySource
    {
        string Name { get; }
        bool SearchEnabled { get; }
        int UrlLabelPosition { get; }
        bool SupportsNotableClips { get; }
        Task<IEnumerable<BookInfo>> SearchBookAsync(string author, string title, CancellationToken cancellationToken = default);
        Task<SeriesInfo> GetSeriesInfoAsync(string dataUrl, CancellationToken cancellationToken = default);
        Task<bool> GetPageCountAsync(BookInfo curBook, CancellationToken cancellationToken = default);
        Task GetExtrasAsync(BookInfo curBook, IProgressBar progress = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Term>> GetTermsAsync(string dataUrl, IProgressBar progress, CancellationToken cancellationToken = default);
        Task<IEnumerable<NotableClip>> GetNotableClipsAsync(string url, HtmlDocument srcDoc = null, IProgressBar progress = null, CancellationToken cancellationToken = default);
        [ItemNotNull] Task<IEnumerable<BookInfo>> SearchBookByAsinAsync(string asin, CancellationToken cancellationToken = default);
    }
}
