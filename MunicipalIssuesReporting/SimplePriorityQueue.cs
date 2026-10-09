using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalIssueReporting
{
    // Lower priority number = more urgent. First in, first out within the same priority.
    public class SimplePriorityQueue<T>
    {
        private readonly SortedDictionary<int, Queue<T>> _buckets =
            new SortedDictionary<int, Queue<T>>();

        public int Count { get; private set; }

        public void Enqueue(T item, int priority)
        {
            Queue<T> bucket;
            if (!_buckets.TryGetValue(priority, out bucket))
            {
                bucket = new Queue<T>();
                _buckets[priority] = bucket;
            }
            bucket.Enqueue(item);
            Count++;
        }

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("The priority queue is empty.");
            return _buckets.Values.First().Peek();
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("The priority queue is empty.");
            var first = _buckets.First();
            T item = first.Value.Dequeue();
            if (first.Value.Count == 0) _buckets.Remove(first.Key);
            Count--;
            return item;
        }
    }
}