# Doujin-Downloader

This version is for donwloading all the doujins from nhentai.net.

More specifically, all the doujins that includes the tag "lolicon".


## How to use

It will go through every page in https://nhentai.net/tag/lolicon/, and download every doujin in the page.

Select path, doujins will be saved there.

Enter which page you want to start download in the "start page", and the page you want to stop in the "end page".

(ex: start page = 1, end page = 100, then it will start download from https://nhentai.net/tag/lolicon/?page=1 to page=100)

The doujin information will be stored in "info" in the same folder with the doujin.


## Notices

- If there occurs any problem when downloading any doujin, this downloader will pass it, 
and save the doujin 6 digit and page in "error" in the path you selected in the begining.

- 1 page requires about 500MB. 
The code will not check if your disk space is enough.
