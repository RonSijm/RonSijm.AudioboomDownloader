# Audioboom Downloader

## Usage

First time you run this program, call it from the command line using this format:

    RonSijm.AudioboomDownloader.exe podcast -p https://audioboom.com/channel/duncantrussellfamilyhour -o "J:\Podcasts\duncantrussellfamilyhour"

## What does this do?

- The first time it will crawl through all the pages, and will cache a cached version of the Audioboom pages.
- It will create a settings.json file which will remember the command line parameters you've used.
- The second time you will run this program, it will check your disk and compare them with podcasts hosted on Audioboom, and it will download everything thats missing.

This will allow you to configure the tool once, run the tool with windows or on a task scheduler or something, and always create a local cache version of your favorite podcasts. Just in case for whatever reason your favorite podcast is ever pulled from the internet, you'll have a local copy reserved.

## Sidenotes

The program uses from weird libraries like a shell progress bar, mostly for my own random experiments to use some libraries...