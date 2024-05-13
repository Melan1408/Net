import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Movie} from "../shared/models/movie";

const API_URL = 'https://localhost:7205/v1.0/';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  getMovies(): Observable<Array<Movie>> {
    return this.http.get<Array<Movie>>(API_URL + 'Movie', { responseType: 'json' });
  }
}
