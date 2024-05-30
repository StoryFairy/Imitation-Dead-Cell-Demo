using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace Edgar.Unity
{
    /// <summary>
    /// Base class for level generators.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public abstract class LevelGeneratorBase<TPayload> : VersionedMonoBehaviour, ILevelGenerator where TPayload : class
    {
        private readonly Random seedsGenerator = new Random();

        protected readonly PipelineRunner<TPayload> PipelineRunner = new PipelineRunner<TPayload>();

        protected abstract bool ThrowExceptionImmediately { get; }

        public bool EnableDiagnostics = false;

        protected virtual Random GetRandomNumbersGenerator(bool useRandomSeed, int seed)
        {
            if (useRandomSeed)
            {
                seed = seedsGenerator.Next();
            }
            
            return new Random(seed);
        }

        public virtual object Generate()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var (pipelineItems, payload) = GetPipelineItemsAndPayload();

            PipelineRunner.Run(pipelineItems, payload, EnableDiagnostics);

            return payload;
        }

        public virtual IEnumerator GenerateCoroutine()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var (pipelineItems, payload) = GetPipelineItemsAndPayload();

            var pipelineIterator = PipelineRunner.GetEnumerator(pipelineItems, payload, EnableDiagnostics);

            if (Application.isPlaying)
            {
                var pipelineCoroutine = this.StartSmartCoroutine<TPayload>(pipelineIterator, ThrowExceptionImmediately);

                yield return pipelineCoroutine.Coroutine;
                yield return pipelineCoroutine.Value;
            }
            else
            {
                while (pipelineIterator.MoveNext())
                {
                }

                yield return payload;
            }
        }

        protected abstract (List<IPipelineTask<TPayload>> pipelineItems, TPayload payload) GetPipelineItemsAndPayload();
    }
}