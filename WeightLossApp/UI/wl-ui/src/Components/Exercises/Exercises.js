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

	const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		Section: false,
		Name: false,
		Length: false,
		BurntCalories: false,
		NumberOfReps: false
	});

	// Get exercises data
	function getExercises() {
		fetch(constants.API_URL + "Exercise")
			.then(res => res.json())
			.then(data => setExercises(data));
	}
}

export default Exercises;
