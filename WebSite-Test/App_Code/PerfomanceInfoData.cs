using System;
using System.Runtime.InteropServices;

[Serializable]
public class PerfomanceInfoData
{
    // data in pages
    public long CommitTotalPages { internal set; get; }
    public long CommitLimitPages { internal set; get; }
    public long CommitPeakPages { internal set; get; }

    // data in bytes
    public long PhysicalTotalMb { internal set; get; }
    public long PhysicalAvailableMb { internal set; get; }
    public decimal PhysicalPercentFree { internal set; get; }
    public decimal PhysicalOccupied { internal set; get; }

    public long SystemCacheMb { internal set; get; }
    public long KernelTotalMb { internal set; get; }
    public long KernelPagedMb { internal set; get; }
    public long KernelNonPagedMb { internal set; get; }
    public long PageSizeMb { internal set; get; }

    // counters
    public int HandlesCount { internal set; get; }
    public int ProcessCount { internal set; get; }
    public int ThreadCount { internal set; get; }

    public string GetAll()
    {
        return $"CommitTotalPages: {CommitTotalPages} <br />" +
               $"CommitLimitPages: {CommitLimitPages} <br />" +
               $"CommitPeakPages: {CommitPeakPages} <br />" +

               $"PhysicalTotalMb: {PhysicalTotalMb} <br />" +
               $"PhysicalAvailableMb: {PhysicalAvailableMb} <br />" +
               $"PhysicalPercentFree: {PhysicalPercentFree} <br />" +
               $"PhysicalOccupied: {PhysicalOccupied} <br />" +

               $"SystemCacheMb: {SystemCacheMb} <br />" +
               $"KernelTotalMb: {KernelTotalMb} <br />" +
               $"KernelPagedMb: {KernelPagedMb} <br />" +
               $"KernelNonPagedMb: {KernelNonPagedMb} <br />" +
               $"PageSizeMb: {PageSizeMb} <br />" +

               $"HandlesCount: {HandlesCount} <br />" +
               $"ProcessCount: {ProcessCount} <br />" +
               $"ThreadCount: {ThreadCount}";
    }
}

public static class PsApiWrapper
{
    [DllImport("psapi.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetPerformanceInfo([Out] out PsApiPerformanceInformation performanceInformation, [In] int size);

    [StructLayout(LayoutKind.Sequential)]
    private struct PsApiPerformanceInformation
    {
        private readonly int Size;
        public IntPtr CommitTotal;
        public IntPtr CommitLimit;
        public IntPtr CommitPeak;
        public IntPtr PhysicalTotal;
        public IntPtr PhysicalAvailable;
        public IntPtr SystemCache;
        public IntPtr KernelTotal;
        public IntPtr KernelPaged;
        public IntPtr KernelNonPaged;
        public IntPtr PageSize;
        public int HandlesCount;
        public int ProcessCount;
        public int ThreadCount;
    }

    public static PerfomanceInfoData GetPerformanceInfo()
    {
        var data = new PerfomanceInfoData();
        var perfInfo = new PsApiPerformanceInformation();

        if (!GetPerformanceInfo(out perfInfo, Marshal.SizeOf(perfInfo))) return data;

        // data in pages
        data.CommitTotalPages = perfInfo.CommitTotal.ToInt64();
        data.CommitLimitPages = perfInfo.CommitLimit.ToInt64();
        data.CommitPeakPages = perfInfo.CommitPeak.ToInt64();

        // data in bytes
        var pageSize = perfInfo.PageSize.ToInt64();

        data.PhysicalTotalMb = perfInfo.PhysicalTotal.ToInt64() * pageSize / 1048576;
        data.PhysicalAvailableMb = perfInfo.PhysicalAvailable.ToInt64() * pageSize / 1048576;
        data.PhysicalPercentFree = (decimal)data.PhysicalAvailableMb / data.PhysicalTotalMb * 100;
        data.PhysicalOccupied = 100 - data.PhysicalPercentFree;

        data.SystemCacheMb = perfInfo.SystemCache.ToInt64() * pageSize / 1048576;
        data.KernelTotalMb = perfInfo.KernelTotal.ToInt64() * pageSize / 1048576;
        data.KernelPagedMb = perfInfo.KernelPaged.ToInt64() * pageSize / 1048576;
        data.KernelNonPagedMb = perfInfo.KernelNonPaged.ToInt64() * pageSize / 1048576;
        data.PageSizeMb = pageSize / 1048576;

        // counters
        data.HandlesCount = perfInfo.HandlesCount;
        data.ProcessCount = perfInfo.ProcessCount;
        data.ThreadCount = perfInfo.ThreadCount;

        return data;
    }
}