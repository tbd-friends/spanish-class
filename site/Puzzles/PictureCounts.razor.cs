using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.DragDrop.Core;
using Microsoft.AspNetCore.Components;
using site.Models;

namespace site.Puzzles
{
    public class PictureCountsModel : ComponentBase, IDisposable
    {
        [Parameter] public EventCallback OnComplete { get; set; }
        [Parameter] public PictureCountsPuzzle Puzzle { get; set; }
        [Inject] public DragDropService DragDrop { get; set; }
        protected bool IsComplete { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            DragDrop.StateHasChanged += DragDropOnStateHasChanged;
        }

        private void DragDropOnStateHasChanged()
        {
            var items = DragDrop.GetDraggablesForDropzone("Answers");

            IsComplete = items.Select(i => i.Tag).SequenceEqual(Puzzle.Key);

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
