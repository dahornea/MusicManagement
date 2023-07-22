import{
    Button,
    Card,
    CardActions,
    CardContent,
    IconButton,
    TextField,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
} from "@mui/material";
import { Container } from "@mui/system";
import { useEffect, useState } from "react";
import {Link, useNavigate, useParams} from "react-router-dom";
import {BACKEND_API_URL} from "../../constants";
import {Album} from "../../models/Album";
import {Artist} from "../../models/Artist";
import { RecordLabel } from "../../models/RecordLabel";
import EditIcon from '@mui/icons-material/Edit';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import axios, { AxiosError } from "axios";
import React from "react";

export const AddAlbum = () => {
    const navigate = useNavigate();
    const [Artist, setArtist] = useState<Artist[]>([]);
    const [RecordLabel, setRecordLabel] = useState<RecordLabel[]>([]);

    const [album, setAlbum] = useState<Album>({
        Title: "",
        Genre:"",
        YearOfRelease: "",
        Price: 0,
        NumberOfTracks: 0,
        ArtistId: 1,
        RecordLabelId: 1,
    });

    const addAlbum = async (event: {preventDefault: () => void}) => {
        event.preventDefault();
        try{
            await axios
            .post(`${BACKEND_API_URL}/Album`, album)
            .then(() => {
                alert("Album added successfully");
        })
        .catch((reason: AxiosError) => {
            console.log(reason.message);
            alert(reason.response?.data);
        });
        navigate("/albums");
        } catch (error) {
            console.log(error);
            alert("Failed to add album");
        }
    };

    useEffect(() => {
        const fetchArtists = async () => {
            try{
                const response = await fetch(`${BACKEND_API_URL}/Artist`);
                const data = await response.json();
                setArtist(data);
            }catch (error) {
                console.log(error);
            }
        };
        fetchArtists();
    }, []);

    return (
        <Container>
            <Card>
                <CardContent>
                    <IconButton component= {Link} sx= {{mr:3}} to = "/albums">
                        <ArrowBackIcon />
                    </IconButton>{" "}
                    <form onSubmit={addAlbum}>
                        <TextField
                            id="Title"
                            label = "Title"
                            variant = "outlined"
                            fullWidth
                            sx={{mb:2}}
                            onChange= {(event) => {
                                setAlbum({
                                    ...album,
                                    Title: event.target.value})}}
                                />
                        <TextField
                            id="Genre"
                            label = "Genre"
                            variant = "outlined"
                            fullWidth
                            sx={{mb:2}}
                            onChange= {(event) => {
                                setAlbum({
                                    ...album,
                                    Genre: event.target.value})}}
                                />
                        <TextField
                            id="YearOfRelease"
                            label = "YearOfRelease"
                            variant = "outlined"
                            fullWidth
                            sx={{mb:2}}
                            onChange= {(event) => {
                                setAlbum({
                                    ...album,
                                    YearOfRelease: event.target.value})}}
                                />
                        <TextField
                            id="Price"
                            label = "Price"
                            variant = "outlined"
                            fullWidth
                            type="number"
                            sx={{mb:2}}
                            onChange= {(event) => {
                                setAlbum({
                                    ...album,
                                    Price: Number(event.target.value)})}}
                                />
                        <TextField
                            id="NumberOfTracks"
                            label = "NumberOfTracks"
                            variant = "outlined"
                            fullWidth
                            type="number"
                            sx={{mb:2}}
                            onChange= {(event) => {
                                setAlbum({
                                    ...album,
                                    NumberOfTracks: Number(event.target.value)})}}
                                />
                        <FormControl fullWidth>
                            <InputLabel id="Artist">Artist</InputLabel>
                            <Select
                                labelId="Artist"
                                id="ArtistId"
                                label="Artist"
                                value={album.ArtistId}
                                variant="outlined"
                                fullWidth
                                sx={{mb:2}}
                                onChange= {(event) => {
                                    setAlbum({
                                        ...album,
                                        ArtistId: Number(event.target.value),
                                    })
                                }}
                                >
                                {Artist.map((artist) => (
                                    <MenuItem key={artist.Id} value={artist.Id}>
                                        {artist.Name}
                                    </MenuItem>
                                ))}

                                    </Select>
                        </FormControl>

                        <Button type="submit">Add Album</Button>
                    </form>
                </CardContent>
                <CardActions></CardActions>
            </Card>
        </Container>
    );
};


