import{
    TableContainer,
    Paper,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    CircularProgress,
    Container,
    IconButton,
    Tooltip,
} from '@mui/material';

import { useEffect, useState } from 'react';
import {BACKEND_API_URL} from '../../constants';
import {Album} from '../../models/Album';
import { Artist } from '../../models/Artist';
import { RecordLabel } from '../../models/RecordLabel';
import React from 'react';

export const FilterAlbumsByPrice = () => {
    const [loading, setLoading] = useState(true);
    const [albums, setAlbums] = useState([]);

    useEffect(() => {
        fetch(`${BACKEND_API_URL}/Album/filter/price`)
        .then(response => response.json())
        .then(data => {
            setAlbums(data);
            setLoading(false);
        });
    }, []);

    return(
        <Container>
            <h1>Albums that cost more than a certain price</h1>
            {loading && <CircularProgress />}
            {!loading && albums.length === 0 && <p>No albums found</p>}
            {!loading && albums.length > 0 && (
                <TableContainer component={Paper}>
                    <Table sx={{minWidth:900}} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>#</TableCell>
                                <TableCell align="left">Title</TableCell>
                                <TableCell align="left">Genre</TableCell>
                                <TableCell align="left">YearOfRelease</TableCell>
                                <TableCell align="left">Price</TableCell>
                                <TableCell align="left">NumberOfTracks</TableCell>
                                <TableCell align="left">Artist</TableCell>
                                <TableCell align="left">RecordLabel</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {albums.map((album: Album, index) => (
                                <TableRow key={album.id}>
                                    <TableCell component="th" scope="row">
                                        {index + 1}
                                    </TableCell>
                                    <TableCell align="left">{album.title}</TableCell>
                                    <TableCell align="left">{album.genre}</TableCell>
                                    <TableCell align="left">{album.yearOfRelease}</TableCell>
                                    <TableCell align="left">{album.price}</TableCell>
                                    <TableCell align="left">{album.numberOfTracks}</TableCell>
                                    <TableCell align="left">{album.Artist?.Name}</TableCell>
                                    <TableCell align="left">{album.RecordLabel?.Name}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </Container>
    );

};