using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


namespace TestAndBenchmark
{
    public partial class Benchmarks : ObservableObject
    {
        [ObservableProperty]
        private String results = String.Empty;

        public async Task RunSelectedAsync(IEnumerable<ConverterDescriptor> selected, IReadOnlyDictionary<ImageLibraryFormat, object> sources)
        {
            foreach (var descriptor in selected)
            {
                if (!sources.TryGetValue(descriptor.Attribute.SourceFormat, out var source))
                {
                    AddResult($"{descriptor.DisplayName}: skipped (no loaded source instance for {descriptor.Attribute.SourceFormat})");
                    continue;
                }

                await RunTestsAsync(descriptor.DisplayName, () => ConverterInvoker.InvokeAsync(descriptor, source));
            }
        }

        public async Task RunTestsAsync(String name, Func<Task<object>> action, int cnt = 10)
        {
            Debug.Print("Start test: " + name);
            try
            {
                GC.Collect();
                var mem = GC.GetAllocatedBytesForCurrentThread();
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < cnt; i++)
                {
                    var result = await action();

                    if (result is IDisposable disposable)
                        disposable.Dispose();
                }
                sw.Stop();
                GC.Collect();
                var usage = (float)(GC.GetAllocatedBytesForCurrentThread() - mem) / 1024f;

                AddResult($"{cnt} x {name}: {sw.ElapsedMilliseconds} ms. Memory usage: {usage} KB");
            }
            catch (Exception ex)
            {
                AddResult($"{name}: FAILED - {ex.GetBaseException().Message}");
            }
        }

        private void AddResult(String text)
        {
            Debug.Print($"{text}");
            Results = $"{Results}\r\n{text}";
        }
    }
}
