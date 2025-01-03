/*
Solution addapted from https://aoc.csokavar.hu/2024/16/
*/

namespace AdventOfCode.Y2024.Day16;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.IO;
using Map = Dictionary<System.Numerics.Complex, char>;
using State = (System.Numerics.Complex pos, System.Numerics.Complex dir);

class Solution {

    static readonly Complex North = -Complex.ImaginaryOne;
    static readonly Complex South = Complex.ImaginaryOne;
    static readonly Complex West = -1;
    static readonly Complex East = 1;

    static int FindBestScore(Map map) => Dijkstra(map, Goal(map))[Start(map)];

    static int FindBestSpots(Map map) {
        var dist = Dijkstra(map, Goal(map));
        var start = Start(map);

        // track the shortest paths using the distance map as guideline.
        var q = new PriorityQueue<State, int>();
        q.Enqueue(start, dist[start]);

        var bestSpots = new HashSet<State> { start };
        while (q.TryDequeue(out var state, out var remainingScore)) {
            foreach (var (next, score) in Steps(map, state, forward: true)) {
                var nextRemainingScore = remainingScore - score;
                if (!bestSpots.Contains(next) && dist[next] == nextRemainingScore) {
                    bestSpots.Add(next);
                    q.Enqueue(next, nextRemainingScore);
                }
            }
        }
        return bestSpots.DistinctBy(state => state.pos).Count();
    }

    static Dictionary<State, int> Dijkstra(Map map, Complex goal) {
        // Dijkstra algorithm; works backwards from the goal, returns the
        // distances to _all_ tiles and directions.
        var dist = new Dictionary<State, int>();

        var q = new PriorityQueue<State, int>();
        foreach (var dir in new[]{North, East, West, South}) {
            q.Enqueue((goal, dir), 0);
            dist[(goal, dir)] = 0;
        }

        while (q.TryDequeue(out var cur, out var totalDistance)) {
            foreach (var (next, score) in Steps(map, cur, forward: false)) {
                var nextCost = totalDistance + score;
                if (!dist.TryGetValue(next, out var currentCost) || nextCost < currentCost) {
                    dist[next] = nextCost;
                    q.Enqueue(next, nextCost);
                }
            }
        }
        return dist;
    }

    // returns the possible next or previous states and the associated costs for a given state.
    // in forward mode we scan the possible states from the start state towards the goal.
    // in backward mode we are working backwards from the goal to the start.
    static IEnumerable<(State, int cost)> Steps(Map map, State state, bool forward) {
        foreach (var dir in new[]{North, East, West, South}) {
            if (dir == state.dir) {
                var pos = forward ? state.pos + dir : state.pos - dir;
                if (map.GetValueOrDefault(pos) != '#') {
                    yield return ((pos, dir), 1);
                }
            } else if (dir != -state.dir) {
                yield return ((state.pos, dir), 1000);
            }
        }
    }

    static Complex Goal(Map map) => map.Keys.Single(k => map[k] == 'E');
    static State Start(Map map) => (map.Keys.Single(k => map[k] == 'S'), East);


    public static void Main() {
        var input = File.ReadAllText("input.txt");
        var map = GetMap(input);
        Console.WriteLine("Part one: " + FindBestScore(map));
        Console.WriteLine("Part two: " + FindBestSpots(map));
    }

    static Map GetMap(string input) {
        var map = input.Split("\n").Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
        return (
            from y in Enumerable.Range(0, map.Length)
            from x in Enumerable.Range(0, map[0].Length)
            select new KeyValuePair<Complex, char>(x + y * South, map[y][x])
        ).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

}