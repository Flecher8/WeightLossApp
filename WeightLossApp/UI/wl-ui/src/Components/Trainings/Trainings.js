import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function Trainings() {
	// Trainings data
	const [trainings, setTrainings] = useState([]);
	const [training, setTraining] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	// Get trainings data
	function getTrainings() {
		fetch(constants.API_URL + "Trainigs")
			.then(res => res.json())
			.then(data => setTrainings(data));
	}

	// onLoad function
	useEffect(() => {
		getTrainings();
	}, []);
}

export default Trainings;
