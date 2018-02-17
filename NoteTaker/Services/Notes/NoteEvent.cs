using System;
using NoteTaker.Models;

namespace NoteTaker.Services.Notes
{
    public class NoteEvent : EventArgs
    {
        public NoteEvent(NoteItem note)
        {
            Note = note;
        }

        public NoteItem Note { get; }
    }
}