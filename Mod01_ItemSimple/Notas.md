# Mod01 — Item Simple

## Estructura del mod

```
Mod01_ItemSimple\
├── About\
│   └── About.xml       ← identifica el mod ante RimWorld
└── Defs\
    └── ThingDefs\
        └── Items.xml   ← define el item
```

---

## About\About.xml

Obligatorio en todo mod. RimWorld lo lee para mostrar el mod en la lista de mods del juego.

```xml
<?xml version="1.0" encoding="utf-8"?>
<ModMetaData>
  <name>Aprendizaje 01 - Item Simple</name>
  <author>DevEliotArias</author>
  <packageId>develiotarias.aprendizaje01</packageId>
  <supportedVersions>
    <li>1.6</li>
  </supportedVersions>
  <description>Mod para aprender a hacer mods - Item Simple</description>
</ModMetaData>
```

| Campo | Obligatorio | Qué hace |
|---|---|---|
| `name` | Sí | Nombre visible en la lista de mods |
| `author` | No | Tu nombre |
| `packageId` | Sí | ID único global. Formato: `autor.nombre`, todo minúsculas, sin espacios |
| `supportedVersions` | Sí | Versiones de RimWorld compatibles. Cada versión en un `<li>` |
| `description` | No | Descripción visible en el menú de mods |

**Regla del packageId:** debe ser único en el mundo. Por eso se usa `autor.nombre` — si dos mods tienen el mismo packageId, RimWorld los considera el mismo mod y uno sobreescribe al otro.

---

## Defs\ThingDefs\Items.xml

Define qué es el item y cómo se comporta. RimWorld lee todos los archivos XML dentro de `Defs\` al arrancar.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Defs>

  <ThingDef>
    <defName>Aprendizaje_RocaDorada</defName>
    <label>Roca Dorada</label>
    <description>Una roca brillante de color dorado.</description>
    <category>Item</category>
    <thingClass>ThingWithComps</thingClass>
    <graphicData>
      <texPath>Things/Item/Resource/Gold</texPath>
      <graphicClass>Graphic_Single</graphicClass>
    </graphicData>
    <stackLimit>75</stackLimit>
    <statBases>
      <MarketValue>50000</MarketValue>
      <Mass>0.5</Mass>
      <Flammability>0</Flammability>
    </statBases>
    <tradeability>Sellable</tradeability>
  </ThingDef>

</Defs>
```

### Campos explicados

| Campo | Qué hace |
|---|---|
| `defName` | ID interno único. El juego lo usa para referenciar este item desde otros XMLs o desde C#. Sin espacios, sin caracteres especiales. Convención: `NombreMod_NombreCosa` |
| `label` | Nombre que ve el jugador. Puede tener espacios y acentos |
| `description` | Texto del tooltip cuando el jugador inspecciona el item |
| `category` | Le dice a RimWorld qué tipo de cosa es. Valores: `Item`, `Building`, `Pawn`, `Plant`, `Projectile` |
| `thingClass` | Clase C# que controla el comportamiento. `ThingWithComps` es la base genérica para la mayoría de ítems |
| `graphicData` | Qué textura usar y cómo dibujarla |
| `stackLimit` | Máximo de unidades apilables en una celda |
| `statBases` | Estadísticas base del item (ver tabla abajo) |
| `tradeability` | Si los comerciantes lo manejan. Valores: `None`, `Sellable`, `Buyable`, `All` |

### graphicData

```xml
<graphicData>
  <texPath>Things/Item/Resource/Gold</texPath>
  <graphicClass>Graphic_Single</graphicClass>
</graphicData>
```

- `texPath` — ruta a la textura, relativa a la carpeta `Textures\` del mod o del juego base
- `graphicClass` — cómo se dibuja:
  - `Graphic_Single` — una sola imagen, igual en todas las rotaciones (ítems, recursos)
  - `Graphic_Multi` — imagen diferente por dirección (norte/sur/este/oeste), para edificios
  - `Graphic_Random` — elige una imagen al azar entre varias (plantas, rocas)

### statBases comunes para ítems

| Stat | Qué controla |
|---|---|
| `MarketValue` | Precio en plata |
| `Mass` | Peso en kg, afecta cuánto puede cargar un colono |
| `Flammability` | Inflamabilidad: 0 = ignífugo, 1 = normal, >1 = muy inflamable |
| `MaxHitPoints` | Durabilidad del item |
| `DeteriorationRate` | Qué tan rápido se deteriora al aire libre |
| `Beauty` | Belleza que aporta si está en el suelo |
| `WorkToMake` | Tiempo de trabajo para fabricarlo en una mesa |

---

## Notas importantes

- **XML es case-sensitive:** `<stackLimit>` y `<StackLimit>` son distintos. RimWorld ignora silenciosamente los campos que no reconoce.
- **Los Defs no necesitan C#:** todo lo que está en `Defs\` es puro XML, el juego lo interpreta solo.
- **La carpeta `Defs\` puede tener subcarpetas:** RimWorld lee todos los `.xml` dentro de `Defs\` recursivamente. La estructura interna es solo para organizarse.
- **`defName` es para siempre:** si publicas el mod y luego cambias un `defName`, las partidas guardadas que tenían ese item se rompen.
