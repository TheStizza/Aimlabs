Hier kommen zukünftig die einzelnen Texturen rein, die nicht in Verbindung mit bestimmten 3D-Modellen geladen werden müssen.

Erlaubte Dateiformate sind:
* JPG (keine Transparenz, wird auf der GPU komplett entpackt, weil JPG-Kompression nicht mit GPUs funktioniert)
* PNG (erlaubt Transparenz, wird auf der GPU komplett entpackt, weil JPG-Kompression nicht mit GPUs funktioniert)
* DDS (DirectDraw Surface, mit den Kompressionsmethoden DXT1, DXT3 oder DXT5). DDS-Kompression ist ideal für GPUs und schont den Grafikspeicher.

Alle Grafiken sind am effizientesten im GPU-Speicher abgelegt, wenn ihre Auflösung aus der Basis 2 mit beliebigem positiven Exponenten gebildet wird:
- z.B. 2^(10) = 1024 für die Breite und 2^(11) = 2048 für die Höhe

