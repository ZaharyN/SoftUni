function attachGradientEvents() {
    const result = document.getElementById('result');
    const gradient = document.getElementById('gradient');

    gradient.addEventListener('mousemove', (e) => {
        const currentPosition = Number(e.offsetX);
        const gradientWidth = Number(e.currentTarget.clientWidth);

        const percentage = Math.floor(currentPosition / gradientWidth * 100);
        result.textContent = percentage + '%';
    });
}