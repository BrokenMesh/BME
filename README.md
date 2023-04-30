<a name="readme-top"></a>

<div align="center">
<h3 align="center">BrokenMesh Game Engine</h3>

  <p align="center">
    A 2D Game Engine using GLFW and OpenGL. It allows the creation of simple 2D games with Input, Sprites, Animation and Text being handled. 
    <br />
    <a href="https://github.com/BrokenMesh/BME/issues">Report Bug</a>
    ·
    <a href="https://github.com/BrokenMesh/BME/issues">Request Feature</a>
  </p>
</div>


<!-- GETTING STARTED -->
## Getting Started

### Installation
1. Clone the repo
   ```sh
   git clone https://github.com/BrokenMesh/BME.git
   ```
2. Open the Solution
   ```sh
   BME.sln
   ```
3. Build Project

<!-- USAGE EXAMPLES -->
## Usage

Create a new project and add BME as a project dependency.

Create a new class with the name of your game and inherit the Game class.
```C#
public class MyGame : Game
{
  public MyGame(int initialWindowWidth, int initialWindowHeight, stri... 
  
  protected override void Close() { }
  protected override void Initalize() { }
  protected override void LoadContent() { }
  protected override void Render() { }
  protected override void Update() { }

}
```

In the Program.cs file you can instantiate your Game class.  
```C#
class Program
{
    public static void Main() {
        Game _game = new MyGame(800,600, "Game");
        _game.Run();
    }    
}
```

In The `Initalize()` function you can create a new Sprite. but first initialize the scene by calling `GameManager.DefaultSceneSetup()`.

```C#
protected override void LoadContent() {
    GameManager.DefaultSceneSetup();

    Texture _texture = TextureLoader.LoadTexture2D_win("./res/img.bmp", GL_LINEAR);

    SimpleSprite _sprite = new SimpleSprite(_texture);   
    _sprite.position = new Vector2(100, 100);
}
```
Before you can create a Sprite you need to load a texture, this is done with the `TextureLoader.LoadTexture2D_win(file, filter)`. Files can be added to a Bin-Dep directory, you just need to copy the Post-Build Event from the example project. 

You need to add the Sprite to the renderer and call the `render()` function of the renderer. 
```C#
protected override void LoadContent() {
    GameManager.DefaultSceneSetup();

    Texture _texture = TextureLoader.LoadTexture2D_win("./res/img.bmp", GL_LINEAR);

    SimpleSprite _sprite = new SimpleSprite(_texture);   
    _sprite.position = new Vector2(100, 100);

    GameManager.currentScene.batchRenderer.AddSprite(_sprite);
}

protected override void Render() {
    GameManager.currentScene.batchRenderer.Render();
    GameManager.currentScene.batchRenderer.PresentRender();
}
```

You should be able to see your sprite. 

<!-- LICENSE -->
## License

Distributed under the GNU GENERAL PUBLIC LICENSE License. See `LICENSE.txt` for more information.

<!-- CONTACT -->
## Contact

Hicham El-Kord - hichamelkord@gmail.com

Project Link: [https://github.com/BrokenMesh/BME](https://github.com/github_username/repo_name)


<p align="right">(<a href="#readme-top">back to top</a>)</p>





















