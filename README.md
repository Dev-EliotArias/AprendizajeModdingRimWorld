# Aprendizaje de Modding de RimWorld

Repositorio donde voy guardando los mods que hago mientras aprendo a modear RimWorld (versión 1.6 / 1.7).
Cada carpeta `ModNN_Nombre` es un mod independiente que se puede copiar a la carpeta `Mods` del juego.

## Mods

| # | Mod | Qué aprendí | C# |
|---|-----|-------------|----|
| 01 | [ItemSimple](Mod01_ItemSimple) | `ThingDef` de un recurso, receta y proyecto de investigación | No |
| 02 | [MesaAlquimia](Mod02_MesaAlquimia) | Edificio de producción, `WorkGiverDef`, clase propia en C# | Sí |

## Estructura de un mod

```
ModNN_Nombre/
├── About/
│   └── About.xml          # nombre, packageId, versiones soportadas
├── Assemblies/            # DLL compilada (no se sube al repo, se genera al compilar)
├── Defs/                  # XML: ThingDefs, RecipeDefs, ResearchProjectDefs...
├── Source/                # proyecto C# (solo si el mod tiene código)
│   └── NombreProyecto/
│       ├── *.cs
│       └── *.csproj
├── Textures/              # PNG que usa el juego
└── _Bocetos/              # imágenes de trabajo (ignorada por Git)
```

## Cómo compilar un mod con C#

1. Abre el `.slnx` o el `.csproj` de `Source/` en Visual Studio o Rider.
2. Compila en **Release**.
3. La DLL resultante debe terminar en la carpeta `Assemblies/` del mod.

> Las DLL de `Assemblies/` y las carpetas `bin/` y `obj/` están en `.gitignore`.
> Después de clonar el repo hay que compilar cada mod con C# antes de probarlo en el juego.

## Cómo probar un mod en el juego

Copia la carpeta del mod (por ejemplo `Mod02_MesaAlquimia`) a:

```
<carpeta de RimWorld>\Mods\
```

o crea un enlace simbólico para no tener que copiar cada vez (PowerShell como administrador):

```powershell
New-Item -ItemType SymbolicLink -Path "E:\SteamLibrary\steamapps\common\RimWorld\Mods\Mod02_MesaAlquimia" -Target "D:\Modding\RimworldMod\Aprendizaje\Mod02_MesaAlquimia"
```
