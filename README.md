## MusicRename

One issue that arises when files are made for different languages is that the character encodings for different fields of the files can be incomprehensible. This mostly arises from how Windows uses Coding Pages to encode text. Unless your machine uses the same coding page the text will be shown incorrectly and won't make sense this is because the symbol that is shown for a specific decimal value will differ based on the coding page.

# Example
![image](https://github.com/user-attachments/assets/588c6acb-6ca9-44ae-853d-32aef3f08916)
These files were encoded on a machine using coding page 1251.

Using the .NET Encoding class this program takes a string of text, first encodes it back to CP1251 (as the string from the file is technically in UTF-16), then takes the CP1251 text and encodes it into Unicode and formats it such that it is stored nicely. To get the different properties as well as set them the TagLib library was used

# Final Result
![image](https://github.com/user-attachments/assets/83a60a98-a8ab-4e5a-b545-1ccce4808fa2)

# Usage
Using the terminal go to the folder in which the project is stored, and run the command. This program is able to run recursively so the top-level folder for the music to be encoded can be provided
``dotnet run --project ./MusicRename [directory]``
