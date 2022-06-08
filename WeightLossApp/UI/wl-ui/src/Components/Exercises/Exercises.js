import { useState, useEffect } from "react";
import React from "react";
import constants from "../../constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function Exercises() {
	// Exercises data
	const [exercises, setExercises] = useState([]);
	const [exercise, setExercise] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);
}

export default Exercises;
