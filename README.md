# Foxes and Rabbits
## Játék leírása
- A program egy életjátékot szimulál amiben rókák és nyulak vannak. A program indulásakor megkeresi a program, hogy létezik-e mentési fájl. Ha létezik akkor opcionális az, hogy betölti az előző játékot vagy újat kezd.
A mentett fájl tartalmazza az oszlopok, sorok számát illetve az egész rácsnak a tartalmát valamint az állatok éhségi szintjeit.
Itt még eldönthetjük, hogy véletlenszerűen szeretnénk elhelyezni az állatokat vagy a felhasználó határozhatja meg, hogy hova rakja le a program az állatokat figyelembe véve azt, hogy helyes-e a bevitt pozíció.
- Ha a felhasználó teljesen új játékot kezd akkor a program bekéri a sorok, oszlopok valamint az egyes állatok számát figyelve és kezeli ha hiba lép fel a bemenet során. Az élőlények minden egyes körben vesztenek egy darab éhségi szintet.
Lehetséges állat lehet nyúl vagy róka. Az állatoknak lehetőségük van a szaporodásra ha az egyik körülöttük lévő mezőben van egy nem éhes társa. Miközben szaporodnak nem lépnek egy körig.
- A nyúl füvet eszik aminek három állapota létezik (alacsony,közepes és magas). Minden egyes körben nő a fű minőségi egy szinttel.
A rókák nyulakra vadásznak és minden lépésnél fogy az éhség szintje. Ha egy állat eléri a 0 szintet éhenhal. Ha a róka elér egy bizonyos éhség szintet akkor elkezd vadászni a nyulakra. Az állatok minden lépés elött ellenőrzik a körülöttük lévő mezőket a láthatósági értékeik alapján és arra a mezőre lépnek amire lehetséges, ha több ilyen is van akkor véletlen sorsol neki a program egy ilyen lehetséges mezőt. A szimuláció addig tart amíg az egyik fél kihal teljesen vagy szaporodás képtelen lesz.

## Statisztika
A játékban van statisztika is ami követi az állatok számát, halálok számát és az eltelt körök számát. A statisztika adatai is mentődésre kerülnek a szöveges állományba amit egy új játék kezdetén betölthet a felhasználó ha úgy határoz.


 ## UML-diagram
![image](https://github.com/botikori/Foxes-And-Rabbits/assets/99285276/61b62ba7-f302-4e1c-bb7e-9cac052b9d49)
