# В данной ветке реализован алгоритм адаптивного круиз-контроля (схема 3-3-3)

## Правило вывода

| dS\dV   | медленно   | отлично    | быстро     | 
|---------|------------|------------|------------| 
| **близко**  | притормози | притормози | притормози | 
| **отлично** | отлично    | отлично    | притормози | 
| **далеко**  | ускорься   | отлично    | притормози | 
