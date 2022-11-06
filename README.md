This code implements the **MovieAdvisor** AR personalized movie recommendation system published in [Touching the Explanations: Explaining Movie Recommendation Scores in Mobile Augmented Reality]().

MovieAdvisor is an AR movie PRS that provides a recommendation score for scanned movies based on previously user-liked movies.

This repository contains the C# code for MovieAdvisor developed in Unity3D and the demo app built for Android and IOS phones.

The video illustration of MovieAdvisor can be found at: https://youtu.be/qbtdEYhpu2Q.

## Usage

MovieAdvisor allows users to scan movie posters on the street or in a movie theater with an AR interface. After detecting a movie poster, MovieAdvisor will give the scanned movie a recommendation score. The calculation of the recommendation score is composed of four recommendation factors:
1. preferred actors
2. preferred genres
3. user movie affinity, based on what similar users have also liked
4. third-party movie rating averages

The main view of MovieAdvisor contains basic information about the scanned movie, including the title, storyline, actors, and genres.
It also shows a recommendation score for the movie and allows users to like the scanned movie by clicking on a heart icon next to the movie title.

![](https://i.imgur.com/ruvBQH1.png)

To customize the recommendation, MovieAdvisor allows users to adjust their expectation scores for each recommendation factor in the setting view.

MovieAdvisor allows users to search for movies and edit their favorite movie list.

When users click the heart button on the left side of the screen, the favorite movie list will appear, showing the movies that were added before. Users can remove movies from the list by clicking on a remove icon.

After clicking on a search icon on the left side of the screen, users can enter a movie name, and the system will display the 20 most matching movies from the name. Users can click the heart icon to like or remove the movies from their favorite list.


Users' favorite movie list | The setting view | The Search view
:------------------:|:------------------:|:------------------:|
![](https://i.imgur.com/VdE8GdM.png)|![](https://i.imgur.com/V62r4w2.png)|![](https://i.imgur.com/p5DkJAx.png)

## Development Software & Packages

- Unity 2019.1.10f1
- Vuforia
- TMDB API
- IMDB API

## Demo Applications

- The Android demo app can be found in [build.apk](/RecSys/App/).
- The IOS demo app can be found in [ios_build.apk](/RecSys/App/).

## Citation
Please cite our paper in your publications if our repository helps your research.

## Contact
- Po-Kai Yang (po-kai.yang@kuleuven.be)

## License
The source code is available under the MIT license.
See [LICENSE](/LICENSE) for more information.
