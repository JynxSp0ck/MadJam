using System;
using System.Collections.Generic;

namespace Game.Utility {
    interface Node {
        List<Connection> getConnections();
    }
}
