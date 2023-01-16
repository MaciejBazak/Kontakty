# Kontakty

Aplikacja wykorzystuje SQL Server Express, AspNetCore 6.0.13, EntityFramework, Identity

Aplikacja jest Single Page Application, typu Model View Controller. Z tego powodu składa się ona głównie z modeli, kontrolerów i widoków. 
Widoki przedstawiają to co użytkownik widzi po swojej stronie. Kontrolery opisują jak aplikacja ma się zachować i jakie akcję podjąć. Modele opisują dane.

Przy tworzeniu aplikacji wykorzystano Code First Approach, czyli na początku stowrzono model reprezentujący dane w aplikacji.
Contact.cs jest to klasa, która jest modelem kontaktu przechowywanego w bazie danych. W tej klasie zawarte są informację na temat tabeli kontaktów, 
na podstawie którego Entity Framework później ją tworzy w bazie danych. Opisane w niej są pola, jakie wartości mają przyjmować i jaki typ danych przechowywać.

ContactController jest kontrolerem, który odpowiada za wszystkie akcje związane z kontaktami. W nim znajdują się akcje odpowiadające zwracaniu 
odpowiedniego widoku listy kontaktów zależnie od tego czy użytkownik jest zalogowany czy nie, tworzenia, usuwania, edycji i wyświetlania szczegółów kontaktu.

W folderze Views znajdują sie wszystkie widoki. W folderze Views/Shared znajdują sie widoki, które są dzielone między innymi stronami, np. layout w którym znajduje się
pasek nawigacyjny czy stopka czy np. ListOfContactsPartial w którym znajduje się część paska nawigacyjnego, która zmienia się zależnie od statusu zalogowania.
W folderze Views/Contact znajdują się wszystkie widoki zwiazane z kontaktami : dodawane, tworzenie, edycja, usuwanie, szczegoly.
