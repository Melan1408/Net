import { Component, OnInit } from '@angular/core';
import { UserService } from "../_services/user.service";
import { Movie } from "../shared/models/movie";

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html'
})
export class MoviesComponent implements OnInit {
  movies: Array<Movie> = [];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getMovies().subscribe(x => this.movies = x);
  }
}
