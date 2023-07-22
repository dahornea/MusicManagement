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
} from "@mui/material";
import React from "react";
import { useEffect, useState } from "react";
import {Link} from "react-router-dom";
import {BACKEND_API_URL} from "../../constants";
import {Album} from "../../models/Album";
import ReadMoreIcon from "@mui/icons-material/ReadMore";
import EditIcon from "@mui/icons-material/Edit";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import AddIcon from "@mui/icons-material/Add";

export const GetAllAlbums = () => {
    const [loading, setLoading] = useState(false);
    const [albums, setAlbums] = useState<Album[]>([]);
    const [sorting, setSorting] = useState({
        key: "Title",
        ascending: true,
    });
    function applySorting(key:string, ascending:boolean){
        setSorting({key:key, ascending:ascending});
    }

    useEffect(() => {
        if(albums.length === 0){
            return;
        }
        const currentAlbums = [...albums];

        const sortedCurrentAlbums = currentAlbums.sort((a, b) => {
            return a[sorting.key].localeCompare(b[sorting.key]);
        });

        setAlbums(
            sorting.ascending ? sortedCurrentAlbums : sortedCurrentAlbums.reverse()
        );
    }, [sorting]);

    useEffect(() => {
        setLoading(true);
        fetch(`${BACKEND_API_URL}/Album`)
        .then(response => response.json())
        .then(data => {
            setAlbums(data);
            setLoading(false);
        });
    }, []);

    return (
        <Container>
            <h1>All Albums</h1>

            {loading && <CircularProgress />}
            {!loading && albums.length === 0 && <p>No albums found</p>}
            {!loading && (
                <IconButton component ={Link} sx = {{mr:3}} to = "/albums/create">
                    <Tooltip title = "Create new album" arrow>
                        <AddIcon color = "primary" />
                    </Tooltip>
                </IconButton>
            )}
            {!loading && albums.length > 0 && (
                <TableContainer component = {Paper}>
                    <Table sx={{minWidth:650}} aria-label ="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>#</TableCell>
                                <TableCell
                                    align="left"
                                    style = {{cursor: "pointer"}}
                                    onClick = {() => applySorting("Title", !sorting.ascending)}
                           >
                                    Title
                                </TableCell>
                                <TableCell align="left">Genre</TableCell>
                                <TableCell align="left">YearOfRelease</TableCell>
                                <TableCell align="left">Price</TableCell>
                                <TableCell align="left">NumberOfTracks</TableCell>
                                <TableCell align="left">Artist</TableCell>
                                <TableCell align="left">RecordLabel</TableCell>
                                </TableRow>
                        </TableHead>
                        <TableBody>
                            {albums.map((album, index) => (
                                <TableRow key = {album.id}>
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
                                    <TableCell align="left">
                                        <IconButton
                                            component = {Link}
                                            sx = {{mr:3}}
                                            to = {'/Album/${album.id}/details'}
                                        >
                                            <Tooltip title = "Read more" arrow>
                                                <ReadMoreIcon color = "primary" />
                                            </Tooltip>
                                        </IconButton>
                                        </TableCell>
                                        <TableCell align="left">
                                            <IconButton
                                                component = {Link}
                                                sx = {{mr:3}}
                                                to = {'/Album/${album.id}/edit'}
                                            >
                                                <EditIcon />
                                            </IconButton>
                                        </TableCell>
                                        <TableCell align="left">
                                            <IconButton
                                                component = {Link}
                                                sx = {{mr:3}}
                                                to = {'/Album/${album.id}/delete'}
                                            >
                                                <DeleteForeverIcon sx = {{color: "red"}} />
                                            </IconButton>
                                        </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </Container>
    );
};


