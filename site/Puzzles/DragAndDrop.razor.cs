using System;
using System.Linq;
using Blazor.DragDrop.Core;
using Microsoft.AspNetCore.Components;
using site.Models;

namespace site.Puzzles
{
    public class DragAndDropModel : ComponentBase, IDisposable
    {
        [Inject] public DragDropService DragDrop { get; set; }
        [Parameter] public EventCallback OnComplete { get; set; }
        [Parameter] public DragAndDropPuzzle Puzzle { get; set; }

        public bool IsComplete { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            DragDrop.StateHasChanged += DragDropOnStateHasChanged;
        }
        protected void DragDropOnStateHasChanged()
        {
            IsComplete = Puzzle.Zones.All(z =>
            {
                var items = DragDrop.GetDraggablesForDropzone(z.Name);

                return items.Any() && z.Requires.All(r => items.Select(i => i.Tag).Cast<string>().Contains(r));
            });

            InvokeAsync(StateHasChanged);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DragDrop.StateHasChanged -= DragDropOnStateHasChanged;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
