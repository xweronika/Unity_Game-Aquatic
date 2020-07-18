# Unity_Game-Aquatic

Autorzy: Dominika Świerzy i Weronika Gołaszczyk.

Sposób uruchomienia gry:
W folderze "RELEASE" znajdują się pliki exe do uruchomienia gry.

Wersja Unity: 2019.2.18f1

Opis gry:
Gra "Aquatic" zbudowana jest na bazie silnika Unity 2D. Posiada elementy symulacji fizycznej oraz wykorzystuje wbudowane w silnik Unity komponenty fizyczne, takie jak Rigidbody oraz Physics.Raycast. Wykorzystuje również efekt oparty o system cząsteczkowy. Gra posiada 3 sceny - scenę menu, scenę gry oraz scenę z listą autorów. 
Gra zawiera wprowadzające w nastrój, oceaniczne efekty dźwiękowe oraz grafiki w klimacie morskim. 

Główną postacią gry jest ryba. Sterujemy nią za pomocą myszki. Wokół nas pływają pozostałe ryby. Mniejsze z nich możemy zjadać, czym zyskujemy punkty. Większe ryby mogą zjadać nas, przez co tracimy wszystkie punkty. Natomiast ryby o podobnym rozmiarze do naszej nie mogą nas zjeść, ani nie mogą być zjadane. Im więcej zjemy ryb, tym nasza postać staje się większa. Gdy nie zjadamy nowych ryb, wraz z upływem czasu nasza postać zmniejsza się. Celem gry jest zdobycie jak największej liczby punktów.

Wykorzystana fizyka: 

Rigidbody 
Sterowanie położeniem obiektu poprzez symulację fizyki. Dodanie komponentu Rigidbody do obiektu spowoduje, że jego ruch będzie kontrolowany przez silnik fizyczny Unity. Rigidbody może przyjąć siły i moment obrotowy, aby obiekty poruszały się w realistyczny sposób. Każdy obiekt GameObject musi zawierać Rigidbody, aby podlegał grawitacji, działał pod dodatkowymi siłami poprzez skrypty lub wchodził w interakcję z innymi obiektami za pośrednictwem NVIDIA PhysX silnik fizyki. Nawet bez dodania żadnego kodu obiekt Rigidbody zostanie pociągnięty w dół przez grawitację i zareaguje na zderzenia z nadchodzącymi obiektami. 

Physics.Raycast 
Rzuca promień od punktu origin (punkt początkowy promienia we współrzędnych światowych), w kierunku direction(Kierunek promienia), wzdłuż maxDistance (maksymalna odległość, na jaką promień powinien sprawdzać kolizje), do wszystkich zderzaczy (colliders) na Scenie.



![1](https://user-images.githubusercontent.com/44413511/87860198-dd396100-c93b-11ea-94d5-0551a4a908d7.png)

_______________________________________________________________

![2](https://user-images.githubusercontent.com/44413511/87860212-eaeee680-c93b-11ea-8e92-c8a96b48c712.png)

_______________________________________________________________

![3](https://user-images.githubusercontent.com/44413511/87860224-f8a46c00-c93b-11ea-99dc-a4441faa8c5c.png)

_______________________________________________________________

![4](https://user-images.githubusercontent.com/44413511/87860225-fcd08980-c93b-11ea-8b61-5f2886c991a5.png)
