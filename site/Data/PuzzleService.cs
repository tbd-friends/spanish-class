using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using site.Infrastructure;
using site.Models;

namespace site.Data
{
    public class PuzzleService
    {
        public PuzzleSet LoadPuzzleSet(string name)
        {
            var @set = JsonConvert.DeserializeObject<JObject>(
                File.ReadAllText(Path.Combine(AppContext.BaseDirectory, @"Data", name)));

            return new PuzzleSet()
            {
                Name = (string)@set["name"],
                Description = (string)@set["description"],
                Puzzles = GetPuzzlesFromFiles(((JArray)@set["puzzles"]).AsEnumerable().Select(s =>
                  Path.Combine(AppContext.BaseDirectory, @"Data\Puzzles", s))).ToList()
            };
        }

        private IEnumerable<Puzzle> GetPuzzlesFromFiles(IEnumerable<string> paths)
        {
            var results =
                from f in paths
                let content = File.ReadAllText(f)
                let puzzle = JsonConvert.DeserializeObject<Puzzle>(content)
                select new
                {
                    PuzzleType = puzzle.Type.ToLower(),
                    Content = content
                };

            foreach (var result in results)
            {
                switch (result.PuzzleType)
                {
                    case "aorb":
                        yield return JsonConvert.DeserializeObject<AorBPuzzle>(result.Content);
                        break;
                    case "picturecounts":
                        yield return JsonConvert.DeserializeObject<PictureCountsPuzzle>(result.Content);
                        break;
                    case "draganddrop":
                        yield return JsonConvert.DeserializeObject<DragAndDropPuzzle>(result.Content);
                        break;
                }
            }
        }
    }
}