import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function SectionTraining() {
	// SectionTraining data
	const [sectionTraining, setSectionTraining] = useState([]);
	const [Section, setSection] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		Type: false
	});

	// Get SectionTraining data
	function getSectionTraining() {
		fetch(constants.API_URL + "SectionTraining")
			.then(res => res.json())
			.then(data => setSectionTraining(data));
	}
}

export default SectionTraining;
