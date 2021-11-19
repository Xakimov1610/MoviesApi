### `Genre`

- GET - /api/genre
    - returns all genres
- GET - api/genre/{id:Guid}
    - return genre with given id
- POST - /api/genre
    - post 'title' of genre in request body
- DELETE - /api/genre/{id}
    - delete genre with given id

### `Actor`

- GET - /api/actor
    - returns all actors
- GET - api/actor/{id:Guid}
    - return actor with given id
- POST - /api/actor
    - post 'actor' in request body
- DELETE - /api/actor/{id}
    - delete actor with given id
- PUT - /api/actor/{id}
    - send actor with id in the body. Validate route and body ids

### `Movie`

- GET - /api/movie
    - returns all movies
- GET - api/movie/{id:Guid}
    - return movie with given id
- POST - /api/movie
    - post 'movie' in request body
- POST /api/movie/{id}/thumbnails
    - post multiple image files (.png, .jpg, .svg) to this route. Attach Image files to movie
        - create new Image entity.
            - -Id: Guid
            - Data: byte[]
            - MovieId: Guid
        - add Images: ICollection<Image> to Movie entity
- DELETE - /api/movie/{id}
    - delete movie with given id
- PUT - /api/movie/{id}
    - send movie with id in the body. Validate route and body ids

On delete, DO NOT cascade, except Image entities.
