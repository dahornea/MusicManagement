import{
    Container,
    Card,
    CardContent,
    IconButton,
    CardActions,
    Button,
} from "@mui/material";
import {Link, useNavigate, useParams} from "react-router-dom";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import axios, {AxiosError} from "axios";
import {BACKEND_API_URL} from "../../constants";
import React from "react";

export const DeleteAlbum = () => {
    const {albumId} = useParams();
    const navigate = useNavigate();

    const handleDelete = async (event : {preventDefault: () => void}) => {
        event.preventDefault();
        await axios
        .delete(`${BACKEND_API_URL}/Album/${albumId}`)
        .then(() => {
            alert("Album deleted successfully");
        })
        .catch((reason: AxiosError) => {
            console.log(reason.message);
            alert(reason.response?.data);
        });

        navigate("/albums");
    };

    const handleCancel = async (event : {preventDefault: () => void}) => {
        event.preventDefault();
        navigate("/albums");
    };

    return (
        <Container>
            <Card>
                <CardContent>
                    <IconButton component={Link} sx={{mr:3}} to="/Album">
                        <ArrowBackIcon />
                    </IconButton>{" "}
                    Are you sure you want to delete this album?
                </CardContent>
                <CardActions>
                    <Button onClick={handleDelete} variant="contained">Yes</Button>
                    <Button onClick={handleCancel} variant="contained">No</Button>
                </CardActions>
            </Card>
        </Container>
    );
};