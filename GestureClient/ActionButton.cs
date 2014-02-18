using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureClient
{
    class ActionButton
    {
        enum Color { Red, Green, Blue};
        enum Alphabet { Q, W, E, R, T, Y, U, I, O, P, A, S, D, F, G, H, J, K, L, Z, X, C, V, B, N, M } ;
        enum Number { Val0 = 0, Val1 = 1, Val2 = 2, Val3 = 3, Val4 = 4, Val5 = 5, Val6 = 6, Val7 = 7, Val8 = 8, Val9 = 9 };
        enum Function { Tab, CapsLock, LShift, LCtrl, Fn, Windows, LAlt, RAly, RCtrl, RShift, UpArrow, DownArrow, RightArrow, LeftArrow };
        // has more entries
        enum Type { button, joystick };

        Color color;
        Type type;
    }
}
