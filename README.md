# Foxes and Rabbits

## A projekt elindítása

A projekt klónozása githubról
```
git clone https://github.com/botikori/Foxes-And-Rabbits.git
```

Belépés a projekt directory-ba
```
cd ./Foxes-And-Rabbits/FoxesAndRabbits/FoxesAndRabbits/
```

A projekt futtatása
```
dotnet run --property WarningLevel=0
```

## Játék leírása
- A program egy életjátékot szimulál amiben rókák és nyulak vannak. A program indulásakor megkeresi a program, hogy létezik-e mentési fájl. Ha létezik akkor opcionális az, hogy betölti az előző játékot vagy újat kezd.
A mentett fájl tartalmazza az oszlopok, sorok számát illetve az egész rácsnak a tartalmát valamint az állatok éhségi szintjeit és a statisztikát.
- Ha a felhasználó teljesen új játékot kezd akkor a program bekéri a sorok, oszlopok valamint az egyes állatok számát figyelve és kezeli ha hiba lép fel a bemenet során. Az állatok pozícióját ki lehet választani vélenszerűen vagy manuálisan. Az élőlények minden egyes körben vesztenek egy darab éhségi szintet.
Lehetséges állat lehet nyúl vagy róka. Az állatoknak lehetőségük van a szaporodásra ha az egyik körülöttük lévő mezőben van egy nem éhes társa. Miközben szaporodnak nem lépnek egy körig.
- A nyúl füvet eszik aminek három állapota létezik (alacsony,közepes és magas). Minden egyes körben nő a fű minősége egy szinttel amely a tápértékét is növeli(0/1/2).
A rókák nyulakra vadásznak és minden lépésnél fogy az éhség szintje. Ha egy állat eléri a 0 szintet éhenhal. Ha a róka elér egy bizonyos éhség szintet akkor elkezd vadászni a nyulakra. Az állatok minden lépés elött ellenőrzik a körülöttük lévő mezőket a láthatósági értékeik alapján és arra a mezőre lépnek amire lehetséges, ha több ilyen is van akkor véletlen sorsol neki a program egy ilyen lehetséges mezőt. A szimuláció addig tart amíg az egyik fél kihal teljesen vagy szaporodás képtelen lesz.
- Minden körben a felhasználó lehetőséget kap a mentésre és kilépésre.
- Egy adott körben minden körben kirajzolódik a keret ahol sárga a nyúl, zöld a fű és piros a róka.
- A fű tápértéke és nagysága az lehet #,X,O (2/1/0 tápérték).


## Statisztika
A játékban van statisztika is ami követi az állatok számát, halálok számát és az eltelt körök számát.

 ## UML-diagram
![image_class_diagram,](https://github.com/botikori/Foxes-And-Rabbits/assets/54451878/95edaf93-5ab3-4082-895f-d9a8d4635eda)

# Tesztelési terv
## A program elindulása
Ha korábban mentésre került egy játék (game.txt létezik) akkor elvárjuk, hogy közli a program a felhasználóval ezt,
és felajánlja neki a döntést, hogy szeretné-e vissza tölteni  a korábban mentésre került állapotot.
Itt kétféle választ fogadunk el ami a 'i' és 'n' karakter. Ha hibás a bevitel akkor addig kérdezi a felhasználót amíg helyes választ nem ad. Ha 'i' akkor betöltésre kerül a fájl, ha 'n' akkor pedig lefut a program mintha nem lett volna mentés. A betöltés tartalmazza a fontosabb adatokat a játékról mint például az állatok számát, a keret összes mezőjét valamint az élőlények éhség szintjét és a statisztikát.

## Keret elkészítése 
Az első kötelező adat megadása a keretnek a méretei. Az első adat az oszlopok száma utána pedig a sorok száma.
Itt csak pozitív egész típusú számokat fogad el a program ami itt 10 és 20 közötti értéket jelent.

## Az élőnyek megadása
A keret létrehozása után a következő az állatok számának megadása. Külön kérjük be a nyulat és rókát. A bevitt szám csak egész típusú pozitív szám lehet(int) ami legalább kettő, hogy lehetőségük legyen a szaporodásra. Ha hibás a bevitt érték a program addig kérdezgeti amíg nem ad meg helyes adatot.


## Élőlények elhelyezése
Az állatok számának megadása után következik az elhelyezése. Itt csak 'i' vagy 'n' opció létezik. Ha 'i' akkor véletlenszerűen lesznek az állatok elhelyezve a kereten, ha 'n' akkor viszont a felhasználó manuálisan adhatja meg az egyes állatok pozícióját x,y formában. Ha rosszul adja meg a felhasználó a bevitt adatot itt is addig kérdezi a program amíg helyes választ nem ad. A bevitt X illetve Y koordináta nem lehet nagyobb mint a keret szélessége és magassága, nem lehet olyan koordinátát megadni ami már foglalt.

## Jelmagyarázat
A konzolon kiírt kereten láthatóak az entitások valamint a fű. Minden egyes állatnak és fű állapotnak más-más jelölése van.
- a rókák egy piros 'F' betű
- a nyúl egy sárga 'R' betű
- az alacsony fű egy zöld 'O', tápértéke 0
- a közepes fű egy zöld 'X', tápértéke 1
- a magas fű pedig egy zöld '#', tápértéke 2

## Az állatok lépésének logikája
Mindkét állat a lépésük elött ellenőrzik a körülöttük lévő területet amelynek a hatótávja a lépésük hatótávjától függ. A lépésnek a feltétele eltérő az állatoknak de minden állat csak egy olyan mezőre léphet ami üres, kivétel a róka ha vadászik. Minden állat csak akkor eszik ha éhes, vagyis a róka sem vadászik ha tele a bendője. A nyúl éhségszintje 0-5 a rókáé 0-6. Az állatok akkor számítanak éhesnek ha a jelenlegi éhségük megegyezik azzal a számmal amit akkor kapnánk ha maximális éhségüket maradék nélkül lefeleznénk majd 1-et hozzá adnánk.
### Nyúl
- A nyúl ha éhes akkor eszik ha tud.
- Csak akkor tud enni ha a fű tápértéke legalább 1 és ha elfogyasztja azt akkor nem lépi túl a maximális éhségét.
- Ha nem tud enni de éhes akkor megpróbál az ellenőrzött területen belül a lehető legnagyobb tápértékű fűre lépni.
- Ha nem éhes akkor az ellenőrzött területen belül keres más nyulakat akik nem éhesek és még nem szaporodnak.
- Ha talál akkor magát a kiválasztott társát szaporodó üzemmódba rakja. Ez alatt egyik nyúl sem végezhet mozgást.
- A szaporodás csak akkor mehet végbe hogyha van legalább egy szabad hely az ellenőrzött területen belül ahova az újonnan született ivadék kerülhet.
- Ha bármelyik feltétele a szaporodásnak nem tud teljesülni akkor utolsó esetben csak véletlenszerűen lép olyan mezőre ahol nem áll állat.
- Ha az állat lépés képtelen úgy nem mozdul meg.
### Róka
- A róka ha éhes akkor vadászni kezd.
- Csak akkor tud enni ha az ellenőrzött területén belül van nyúl.
- Ha vadászati üzemmódban van és tud enni akkor egy véletlenszerűen ellenőrzött területre ugrik amin van nyúl és megeszi azt.
- A nyúl eltűnik, meghal, a róka pedig a nyúl tápértékével(3) egyenlő mértékkel tölti az éhség szintjét.
- Ha nem tud enni, vagy nem vadászik akkor szaporodó partner után keres. Megfelelő szaporodó partner csak olyan róka lehet amely éppen nem éhes/vadászik(nehogy éhen haljon) és még nem szaporodik senkivel.
- A szaporodás csak akkor mehet végbe hogyha van legalább egy szabad hely az ellenőrzött területen belül ahova az újonnan született ivadék kerülhet.
- Ha bármelyik feltétele a szaporodásnak nem tud teljesülni akkor utolsó esetben csak véletlenszerűen lép olyan mezőre ahol nem áll állat.
- Ha talál akkor magát a kiválasztott társát szaporodó üzemmódba rakja. Ez alatt egyik róka sem végezhet mozgást.
- Ha az állat lépés képtelen úgy nem mozdul meg.


## Egy kör menete
Minden egyes körben az állatok sorban egyesével lépnek. A sorrend a balfeső saroktól a jobbalsó sarokig, elsődlegesen vízszintesen másodlagosan függőlegesen számítandó. Ezután az új keretet kirajzolja a konzolra. Ezután megjelenik egy kisebb összesítés a példányszámokról és a halálozásokról. Az új kör végén program választ vár arra vonatkozóan, hogy folytatódjon a szimuláció vagy pedig mentse-e a folyamatot. A folytatásra 'i' betűt kell beírni amíg a mentésre a "mentes" szót. Ha mentésre kerül sor, a játék végetér és eltárolja az adatokat a szöveges állományba amit generál(game.txt).


## Játék vége
A játék csak akkor érhet véget ha kihal egy faj vagy szaporodás képtelen lesz. Megjelenik a statisztika ami tartalmazza a meghalt állatok számát, nyulak számát, rókák számát és az eltelt körök számát.
