# TicTacToeGame
 test for backend c#

## Методы API

## POST /game

Создает новую игру в крестики-нолики. Возвращает идентификатор созданной игры.

Пример запроса:

```bash
POST /game HTTP/1.1
Host: example.com
```
Пример ответа:

```css
HTTP/1.1 200 OK
Content-Type: application/json

{
    "gameId": "1234"
}
```

## POST /game/{gameId}/move

Совершает ход в игре с заданным идентификатором. Принимает параметры запроса row и column, которые указывают координаты хода. Возвращает информацию о текущем состоянии игры.

Пример запроса:

```bash

POST /game/1234/move?row=0&column=1 HTTP/1.1
Host: example.com
```
Пример ответа:

```bash

HTTP/1.1 200 OK
Content-Type: application/json

{
    "board": [
        ["X", "", ""],
        ["O", "O", "X"],
        ["", "X", ""]
    ],
    "isGameOver": false,
    "message": "Move played successfully"
}
```
## GET /game/{gameId}

Возвращает информацию о состоянии игры с заданным идентификатором.

Пример запроса:

```vbnet

GET /game/1234 HTTP/1.1
Host: example.com
```
Пример ответа:

```bash
HTTP/1.1 200 OK
Content-Type: application/json

{
    "board": [
        ["X", "", ""],
        ["O", "O", "X"],
        ["", "X", ""]
    ],
    "isGameOver": false,
    "message": ""
}
```
Формат сообщений
API поддерживает формат сообщений в формате JSON. При отправке запросов необходимо указывать заголовок Content-Type: application/json.

## Ошибки

API может вернуть следующие ошибки:
```
400 Bad Request: неверный запрос или неверные параметры запроса.
404 Not Found: игра с заданным идентификатором не найдена.
500 Internal Server Error: внутренняя ошибка сервера.
```
