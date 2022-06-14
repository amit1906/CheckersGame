using System;

namespace CheckersGameForms.Controls
{
    interface BoardPiece
    {
        int X { get; set; }
        int Y { get; set; }
        bool IsSelected { get; set; }

    }
}